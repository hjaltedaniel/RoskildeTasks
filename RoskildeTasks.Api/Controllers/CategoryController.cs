using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web.WebApi;
using RoskildeTasks.Api.Models;
using Umbraco.Core.PropertyEditors.ValueConverters;
using Newtonsoft.Json;

namespace RoskildeTasks.Api.Controllers
{
    public class CategoryController : UmbracoApiController
    {
        
        [RoleAuthorize]
        [HttpGet]
        public IHttpActionResult GetAllCategories()
        {
            IContentService cs = Services.ContentService;

            var allContent = cs.GetContentOfContentType(Configurations.CategoryDocType);

            if(allContent.Any())
            {
                List<CategoryItem> allCategories = new List<CategoryItem>();

                foreach (var content in allContent)
                {
                    CategoryItem category = new CategoryItem();
                    category.Id = content.Id;
                    category.Name = content.Name;
                    category.ShortName = content.GetValue("shortName").ToString();

                    var colorString = content.GetValue("categoryColor").ToString();
                    ColorItem color = JsonConvert.DeserializeObject<ColorItem>(colorString);

                    category.Color = color;
                    category.StandardMessage = content.GetValue("standardMessage").ToString();
                    category.isOnlyMessages = content.GetValue<bool>("isOnlyMessages");

                    allCategories.Add(category);
                }

                if (allCategories.Any())
                {
                    return Ok(allCategories);
                }
                else
                {
                    return StatusCode(System.Net.HttpStatusCode.NoContent);
                }
            }
            else
            {
                return StatusCode(System.Net.HttpStatusCode.NoContent);
            }
        }
    }
}
