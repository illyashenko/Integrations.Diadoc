using Diadoc.Api;
using Diadoc.Api.Proto.Events;
using Integrations.Diadoc.Data.Apt.Specifications.Filters;
using Integrations.Diadoc.Data.Monitoring.Models;
using Integrations.Diadoc.Infrastructure.Settings;
using Integrations.Diadoc.Infrastructure.Stores;
using Integrations.Diadoc.Infrastructure.SubServices.TokenService;
using Microsoft.Extensions.Options;
using NonformalizedAttachment = Diadoc.Api.Proto.Events.NonformalizedAttachment;
using Hyphens =  Diadoc.Api.DataXml.Utd820.Hyphens;
using SignedContent = Diadoc.Api.Proto.Events.SignedContent;

namespace Integrations.Diadoc.Infrastructure.SubServices.DocumentBuilders;

public class BuildUserData : IBuildUserData
{
        private CommonSettings Settings { get; }

        private IDiadocApi Api { get; set; }

        private AptStore AptStore { get; set; }
        
        private IAuthToken AuthToken { get; set; }
        
        private EmployeeSettings EmployeeSettings { get; }

        public BuildUserData(IOptions<CommonSettings> settings
            , IDiadocApi api
            , AptStore aptStore
            , IAuthToken authToken)
        {
            this.Settings = settings.Value;
            this.Api = api;
            this.AptStore = aptStore;
            this.AuthToken = authToken;
            this.EmployeeSettings = this.Settings.EmployeeSettings?.First(el=>el.Position == EmployeePosition.AccountManager)!;
        }

        public async Task<MessageToPost> BuildMessageToPost(RequestIdData data)
        {
            var messageToPost = new MessageToPost();
            var sendingDocument = await AptStore.GetDataForSendingDocument(data);
            
            messageToPost.FromBoxId = Settings.FromBoxId;
            await AddNonformalizedDocument(sendingDocument, messageToPost);

            if (sendingDocument.Bill)
            {
                await BuildUserDataUniversalDocument(sendingDocument, messageToPost);
            }
            
            return messageToPost;
        }

        public async Task BuildTest(RequestIdData requestIdData)
        {
            var sendingDocument = await AptStore.GetDataForSendingDocument(requestIdData);

            var doc = await AptStore.GetDataForMessagePost(DocumentTitleFilter.GetFilterDocumentReport(sendingDocument.Key));
        }

        private async Task BuildUserDataUniversalDocument(SendingDocument sendingData, MessageToPost messageToPost)
        {
            var documentDescription =
                await Api.GetDocumentTypesV2Async(await AuthToken.GetAccessToken(this.EmployeeSettings),
                    Settings.FromBoxId);

            var dataUtd =
                await AptStore.GetDataForMessagePost(DocumentTitleFilter.GetFilterDocumentReport(sendingData.Key));

            if (!dataUtd.ValidateBoxId())
            {
                throw new Exception("поле BoxId не соответствует, или пустое!");
            }

            var typeNameId = documentDescription.DocumentTypes.Find(dt => dt.Title == DiadocNameConstants.GetTitle(dataUtd.Upd));
            var documentFunction = typeNameId?.Functions
                .Find(f => f.Name == (dataUtd.Function == Hyphens.UniversalTransferDocumentWithHyphensFunction.СЧФ 
                    ? DiadocNameConstants.DefaultName : dataUtd.Function.ToString()));
            var documentVersion = documentFunction?.Versions.Find(v => v.Version != null);
            var title = documentVersion?.Titles.FirstOrDefault();
            var indexTitle = title?.Index ?? 0;
            
            var documentToXml = new Hyphens.UniversalTransferDocumentWithHyphens
            {
                Sellers = BuilderHelper.GetSellers(Settings.FromBoxId ?? String.Empty),
                Signers = BuilderHelper.GetSigners(Settings.FromBoxId?? String.Empty, Settings.CertificateThumbprint ?? String.Empty),
                Shippers = BuilderHelper.GetShippers(),
                Buyers = BuilderHelper.GetBayer(dataUtd),
                TransferInfo = new Hyphens.TransferInfo
                {
                    OperationInfo = DiadocNameConstants.OperationInfo,
                    Employee = BuilderHelper.GetEmployee(Settings.GetDataEmployee(EmployeePosition.GeneralManager)),
                    TransferBases = BuilderHelper.GetTransferBases(dataUtd)
                },
                DocumentShipments = BuilderHelper.GetShipments(dataUtd),
                DocumentCreator = $"{Settings.OrganizationName} ИНН/КПП {Settings.Inn}/{Settings.Kpp}",
                DocumentDate =dataUtd.DocumentDate.ToString("dd.MM.yyyy"),
                DocumentName = DiadocNameConstants.GetDocumentName(dataUtd.Function),
                DocumentNumber = dataUtd.DocumentNumber,
                Currency = DiadocNameConstants.Currency,
                Function = dataUtd.Function,
                Table = BuilderHelper.GetInvoiceTable(dataUtd)
            };

            var contract = documentToXml.SerializeToXml();

            var documentXml = await Api.GenerateTitleXmlAsync(await AuthToken.GetAccessToken(this.EmployeeSettings),
                Settings.FromBoxId, typeNameId?.Name,
                documentFunction?.Name, documentVersion?.Version,
                indexTitle, contract);

            var attachment = new DocumentAttachment
            {
                SignedContent = new SignedContent
                {
                    Content = documentXml.Content
                },
                TypeNamedId = typeNameId?.Name
            };
            
            messageToPost.AddDocumentAttachment(attachment);
            messageToPost.ToBoxId = dataUtd.BoxToId;
            
            if (dataUtd.Function == Hyphens.UniversalTransferDocumentWithHyphensFunction.СЧФ)
            {
                documentToXml.Function = Hyphens.UniversalTransferDocumentWithHyphensFunction.ДОП;
                var contractForAct = documentToXml.SerializeToXml();
                var documentXmlForAct = await Api.GenerateTitleXmlAsync(await AuthToken.GetAccessToken(this.EmployeeSettings)
                    , Settings.FromBoxId, DiadocNameConstants.TypeNameId, DiadocNameConstants.DefaultName
                    , documentVersion?.Version, indexTitle, contractForAct);

                var attachmentAct = new DocumentAttachment
                {
                    TypeNamedId = DiadocNameConstants.TypeNameId,
                    SignedContent = new SignedContent
                    {
                        Content = documentXmlForAct.Content
                    }
                };
                
                messageToPost.AddDocumentAttachment(attachmentAct);
            }
        }

        private async Task<DocumentAttachment> BuildUserDataNonformalizedDocument(SendingDocument sendingData)
        {
            var token = await AuthToken.GetAccessToken(this.EmployeeSettings);
            var uploadedFileShelfName = await Api.UploadFileToShelfAsync(token, sendingData.Content);

            var documentAttachment = new DocumentAttachment
            {
                TypeNamedId = DiadocNameConstants.NonFormalizedDocument,
                SignedContent = new SignedContent
                {
                    NameOnShelf = uploadedFileShelfName
                },
                Metadata =
                {
                    new MetadataItem
                    {
                        Key = DiadocNameConstants.KeyName,
                        Value = sendingData.FileName
                    }
                },
                NeedRecipientSignature = true
            };

            return documentAttachment;
        }

        private async Task AddNonformalizedDocument(SendingDocument sendingDocument, MessageToPost messageToPost)
        {
            var dataForMessageToPost =
                await this.AptStore.GetDataNonformalizedDocument(
                    DocumentTitleFilter.GetFilterDocumentReport(sendingDocument.Key));

            var attachment = new NonformalizedAttachment
            {
                DocumentDate = dataForMessageToPost.AttachmentModel?.DocumentDate?.ToString("dd.MM.yyyy"),
                DocumentNumber = dataForMessageToPost.AttachmentModel?.DocumentNumber,
                FileName = sendingDocument.FileName,
                SignedContent = new SignedContent
                {
                    Content = sendingDocument.Content
                },
                NeedRecipientSignature = true
            };

            messageToPost.ToBoxId = dataForMessageToPost.BoxToId;
            messageToPost.NonformalizedDocuments.Add(attachment);
        }
}
