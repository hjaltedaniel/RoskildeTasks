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

namespace RoskildeTasks.Api.Controllers
{
    public class CategoryController : UmbracoApiController
    {
        
        [RoleAuthorize]
        [HttpGet]
        public List<CategoryItem> GetAllCategories()
        {
            IContentService cs = Services.ContentService;

            var allContent = cs.GetContentOfContentType(1063);

            List<CategoryItem> allCategories = new List<CategoryItem>();

            foreach(var content in allContent)
            {
                CategoryItem category = new CategoryItem();
                category.Id = content.Id;
                category.Name = content.Name;
                category.ShortName = content.GetValue("shortName").ToString();
                category.Color = content.GetValue("categoryColor").ToString();
                category.StandardMessage = content.GetValue("standardMessage").ToString();

                allCategories.Add(category);
            }

            return allCategories;

            
        }
    }
}
