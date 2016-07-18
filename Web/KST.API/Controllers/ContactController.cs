using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KST.Core;
using KST.DTO;
using KST.Model;
using KST.Service;
using KST.Util;
using Newtonsoft.Json;

namespace KST.API.Controllers
{
    /// <summary>
    /// 联系人实体控制器
    /// </summary>
    public class ContactController : ApiController
    {
        private SecurityService securityService = ServiceFactory.Instance.SecurityService;
        private UserDataService userDataService = ServiceFactory.Instance.UserDataService;
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(ContactController));

        [HttpGet]
        [HttpPost]
        public HttpResponseMessage Index()
        {
            HttpResponseMessage response = null;
            try
            {
                string cmd = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_CMD);
                if (!string.IsNullOrEmpty(cmd))
                {
                    switch (cmd.ToLower())
                    {
                        case Cmd.CONTACT_GET_BY_ID: response = GetContactByID(Request); break;
                        case Cmd.CONTACT_GET_ALL: response = GetContactByUserID(Request); break;
                        case Cmd.CONTACT_QUERY: response = QueryContact(Request); break;
                        case Cmd.CONTACT_ADD: response = AddContact(Request); break;
                        case Cmd.CONTACT_UPDATE: response = UpdateContact(Request); break;
                        case Cmd.CONTACT_DELETE: response = DeleteContact(Request); break;
                        case Cmd.CONTACT_SYNC: response = SyncContact(Request); break;
                        default: response = Request.CreateResponse(HttpStatusCode.OK, new ServiceInvokeDTO(InvokeCode.SYS_UNKNOW_CMD)); break;
                    }
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, new ServiceInvokeDTO(InvokeCode.SYS_UNKNOW_CMD));
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                response = Request.CreateResponse(HttpStatusCode.OK, new ServiceInvokeDTO(InvokeCode.SYS_INNER_ERROR));
            }
            return response;
        }

        /// <summary>
        /// 分页查询联系人
        /// </summary>
        private HttpResponseMessage QueryContact(HttpRequestMessage request)
        {
            log.Debug(Constant.DEBUG_START);

            string sign = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_SIGN);
            string cmd = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_CMD);
            string random = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_RANDOM);

            string pageIndexString = ApiQueryUtil.QueryArgByGet("page_index");
            string pageSizeString = ApiQueryUtil.QueryArgByGet("page_size");
            string userIDString = ApiQueryUtil.QueryArgByGet("user_id");
            string name = ApiQueryUtil.QueryArgByGet("name");
            string phone = ApiQueryUtil.QueryArgByGet("phone");

            Dictionary<string, string> args = new Dictionary<string, string>() 
            { 
                { Constant.HTTP_HEADER_CMD, cmd},
                { Constant.HTTP_HEADER_RANDOM, random},
                { "page_index", pageIndexString},
                { "page_size", pageSizeString},
                { "user_id", userIDString},
                { "name", name },
                { "phone", phone }
            }.OrderBy(element => element.Key).ToDictionary(o => o.Key, p => p.Value);

            ServiceInvokeDTO<QueryResultDTO<Contact>> result = null;
            try
            {
                // Check sign
                if (securityService.CheckSign(args, Config.ApiSignSecretKey, sign))
                {
                    QueryArgsDTO<Contact> queryDTO = new QueryArgsDTO<Contact>();
                    queryDTO.PageIndex = Convert.ToInt32(pageIndexString);
                    queryDTO.PageSize = Convert.ToInt32(pageSizeString);
                    queryDTO.Model.UserID = string.IsNullOrEmpty(userIDString) ? -1 : Convert.ToInt32(userIDString);
                    queryDTO.Model.Name = name;
                    queryDTO.Model.Phone = phone;

                    result = userDataService.QueryContact(queryDTO);
                }
                else
                {
                    result = new ServiceInvokeDTO<QueryResultDTO<Contact>>(InvokeCode.SYS_SIGN_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<QueryResultDTO<Contact>>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 根据主键ID获取联系人
        /// </summary>
        private HttpResponseMessage GetContactByID(HttpRequestMessage request)
        {
            log.Debug(Constant.DEBUG_START);

            string sign = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_SIGN);
            string cmd = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_CMD);
            string random = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_RANDOM);

            string idString = ApiQueryUtil.QueryArgByGet("id");

            Dictionary<string, string> args = new Dictionary<string, string>() 
            { 
                { Constant.HTTP_HEADER_CMD, cmd},
                { Constant.HTTP_HEADER_RANDOM, random},
                { "id", idString }
            }.OrderBy(element => element.Key).ToDictionary(o => o.Key, p => p.Value);

            ServiceInvokeDTO<Contact> result = null;
            try
            {
                // Check sign
                if (securityService.CheckSign(args, Config.ApiSignSecretKey, sign))
                {
                    result = userDataService.GetContactByID(Convert.ToInt32(idString));
                }
                else
                {
                    result = new ServiceInvokeDTO<Contact>(InvokeCode.SYS_SIGN_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<Contact>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 获取指定用户联系人列表
        /// </summary>
        private HttpResponseMessage GetContactByUserID(HttpRequestMessage request)
        {
            log.Debug(Constant.DEBUG_START);

            string sign = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_SIGN);
            string cmd = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_CMD);
            string random = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_RANDOM);

            string userIDString = ApiQueryUtil.QueryArgByGet("user_id");

            Dictionary<string, string> args = new Dictionary<string, string>() 
            { 
                { Constant.HTTP_HEADER_CMD, cmd},
                { Constant.HTTP_HEADER_RANDOM, random},
                { "user_id", userIDString }
            }.OrderBy(element => element.Key).ToDictionary(o => o.Key, p => p.Value);

            ServiceInvokeDTO<List<Contact>> result = null;
            try
            {
                // Check sign
                if (securityService.CheckSign(args, Config.ApiSignSecretKey, sign))
                {
                    result = userDataService.GetContactByUserID(Convert.ToInt32(userIDString));
                }
                else
                {
                    result = new ServiceInvokeDTO<List<Contact>>(InvokeCode.SYS_SIGN_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<List<Contact>>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 添加联系人
        /// </summary>
        private HttpResponseMessage AddContact(HttpRequestMessage request)
        {
            log.Debug(Constant.DEBUG_START);

            string sign = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_SIGN);
            string cmd = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_CMD);
            string random = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_RANDOM);

            string userIDString = ApiQueryUtil.QueryArgByPost("user_id");
            string name = ApiQueryUtil.QueryArgByPost("name");
            string phone = ApiQueryUtil.QueryArgByPost("phone");
            string email = ApiQueryUtil.QueryArgByPost("email");
            string company = ApiQueryUtil.QueryArgByPost("company");
            string address = ApiQueryUtil.QueryArgByPost("address");
            string remark = ApiQueryUtil.QueryArgByPost("remark");

            Dictionary<string, string> args = new Dictionary<string, string>() 
            { 
                { Constant.HTTP_HEADER_CMD, cmd },
                { Constant.HTTP_HEADER_RANDOM, random },
                { "user_id", userIDString },
                { "name", name },
                { "phone", phone },
                { "email", email },
                { "company", company },
                { "address", address },
                { "remark", remark }
            }.OrderBy(element => element.Key).ToDictionary(o => o.Key, p => p.Value);

            ServiceInvokeDTO<Contact> result = null;
            try
            {
                // Check sign
                if (securityService.CheckSign(args, Config.ApiSignSecretKey, sign))
                {
                    Contact contact = new Contact();
                    contact.UserID = Convert.ToInt32(userIDString);
                    contact.Name = name;
                    contact.Phone = phone;
                    contact.Email = email;
                    contact.Company = company;
                    contact.Address = address;
                    contact.Remark = remark;

                    result = userDataService.AddContact(contact);
                }
                else
                {
                    result = new ServiceInvokeDTO<Contact>(InvokeCode.SYS_SIGN_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<Contact>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 更新联系人
        /// </summary>
        private HttpResponseMessage UpdateContact(HttpRequestMessage request)
        {
            log.Debug(Constant.DEBUG_START);

            string sign = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_SIGN);
            string cmd = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_CMD);
            string random = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_RANDOM);

            string idString = ApiQueryUtil.QueryArgByPost("id");
            string name = ApiQueryUtil.QueryArgByPost("name");
            string phone = ApiQueryUtil.QueryArgByPost("phone");
            string email = ApiQueryUtil.QueryArgByPost("email");
            string company = ApiQueryUtil.QueryArgByPost("company");
            string address = ApiQueryUtil.QueryArgByPost("address");
            string remark = ApiQueryUtil.QueryArgByPost("remark");

            Dictionary<string, string> args = new Dictionary<string, string>() 
            { 
                { Constant.HTTP_HEADER_CMD, cmd },
                { Constant.HTTP_HEADER_RANDOM, random },
                { "id", idString },
                { "name", name },
                { "phone", phone },
                { "email", email },
                { "company", company },
                { "address", address },
                { "remark", remark }
            }.OrderBy(element => element.Key).ToDictionary(o => o.Key, p => p.Value);

            ServiceInvokeDTO result = null;
            try
            {
                // Check sign
                if (securityService.CheckSign(args, Config.ApiSignSecretKey, sign))
                {
                    Contact contact = new Contact();
                    contact.ID = Convert.ToInt32(idString);
                    contact.Name = name;
                    contact.Phone = phone;
                    contact.Email = email;
                    contact.Company = company;
                    contact.Address = address;
                    contact.Remark = remark;

                    result = userDataService.UpdateContact(contact);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_SIGN_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 删除联系人
        /// </summary>
        private HttpResponseMessage DeleteContact(HttpRequestMessage request)
        {
            log.Debug(Constant.DEBUG_START);

            string sign = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_SIGN);
            string cmd = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_CMD);
            string random = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_RANDOM);

            string idString = ApiQueryUtil.QueryArgByPost("id");

            Dictionary<string, string> args = new Dictionary<string, string>() 
            { 
                { Constant.HTTP_HEADER_CMD, cmd },
                { Constant.HTTP_HEADER_RANDOM, random },
                { "id", idString }
            }.OrderBy(element => element.Key).ToDictionary(o => o.Key, p => p.Value);

            ServiceInvokeDTO result = null;
            try
            {
                // Check sign
                if (securityService.CheckSign(args, Config.ApiSignSecretKey, sign))
                {
                    int id = Convert.ToInt32(idString);
                    result = userDataService.DeleteContact(id);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_SIGN_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 同步联系人
        /// </summary>
        private HttpResponseMessage SyncContact(HttpRequestMessage request)
        {
            log.Debug(Constant.DEBUG_START);

            string sign = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_SIGN);
            string cmd = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_CMD);
            string random = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_RANDOM);

            string userIDString = ApiQueryUtil.QueryArgByPost("user_id");
            string newContactJson = ApiQueryUtil.QueryArgByPost("contacts_json");
            string syncTypeString = ApiQueryUtil.QueryArgByPost("sync_type");

            Dictionary<string, string> args = new Dictionary<string, string>() 
            { 
                { Constant.HTTP_HEADER_CMD, cmd },
                { Constant.HTTP_HEADER_RANDOM, random },
                { "user_id", userIDString },
                { "contacts_json", newContactJson },
                { "sync_type", syncTypeString }
            }.OrderBy(element => element.Key).ToDictionary(o => o.Key, p => p.Value);

            ServiceInvokeDTO result = null;
            try
            {
                // Check sign
                if (securityService.CheckSign(args, Config.ApiSignSecretKey, sign))
                {
                    int syncType = Convert.ToInt32(syncTypeString);
                    if (syncType == Service.Constant.CONTACT_SYNC_TYPE_MERGE || syncType == Service.Constant.CONTACT_SYNC_TYPE_DELELTE_INSERT)
                    {
                        int userID = Convert.ToInt32(userIDString);
                        List<Contact> newContacts = JsonConvert.DeserializeObject<List<Contact>>(newContactJson);

                        result = userDataService.SyncContact(userID, newContacts, syncType);
                    }
                    else
                    {
                        result = new ServiceInvokeDTO(InvokeCode.SYS_ARG_ERROR);
                    }
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_SIGN_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
