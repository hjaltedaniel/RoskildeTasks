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
using System.IO;
using Newtonsoft.Json;

namespace RoskildeTasks.Api.Controllers
{
    public class RessourceController : UmbracoApiController
    {
        [RoleAuthorize]
        [HttpGet]
        public IHttpActionResult GetAllRessources()
        {
            var currentUser = Members.CurrentUserName;
            IContentService cs = Services.ContentService;

            var everyRessource = cs.GetContentOfContentType(Configurations.RessourceDocType);

            if (everyRessource != null)
            {
                List<RessourceItem> usersRessources = new List<RessourceItem>();

                foreach (var ressource in everyRessource)
                {
                    string ressourceGroups = ressource.GetValue("memberAccess").ToString();

                    if (Functions.IsMemberInGroups(ressourceGroups, currentUser))
                    {
                        RessourceItem usersRessource = new RessourceItem();
                        usersRessource.Name = ressource.Name;
                        usersRessource.Url = ressource.GetValue("file").ToString();
                        usersRessource.Filetype = Path.GetExtension(usersRessource.Url).Replace(".", "");

                        var categoryUri = ressource.GetValue("category");
                        var thisCategory = cs.GetById(Umbraco.TypedContent(categoryUri).Id);
                        CategoryItem ressourceCategory = new CategoryItem();
                        ressourceCategory.Id = thisCategory.Id;
                        ressourceCategory.Name = thisCategory.Name;
                        ressourceCategory.ShortName = thisCategory.GetValue("shortName").ToString();

                        var colorString = thisCategory.GetValue("categoryColor").ToString();
                        ColorItem color = JsonConvert.DeserializeObject<ColorItem>(colorString);
                        ressourceCategory.Color = color;

                        ressourceCategory.StandardMessage = thisCategory.GetValue("standardMessage").ToString();
                        ressourceCategory.isOnlyMessages = thisCategory.GetValue<bool>("isOnlyMessages");
                        usersRessource.Category = ressourceCategory;

                        usersRessources.Add(usersRessource);
                    }
                }
                return Ok(usersRessources);
            }
            else
            {
                return StatusCode(System.Net.HttpStatusCode.NoContent);
            }
        }

        [RoleAuthorize]
        [HttpGet]
        public IHttpActionResult GetRessourcesForCategory(int categoryId)
        {
            var currentUser = Members.CurrentUserName;
            IContentService cs = Services.ContentService;

            var everyRessource = cs.GetContentOfContentType(Configurations.RessourceDocType);

            List<RessourceItem> categoryRessources = new List<RessourceItem>();

            foreach (var ressource in everyRessource)
            {
                string ressourceGroups = ressource.GetValue("memberAccess").ToString();

                if (Functions.IsMemberInGroups(ressourceGroups, currentUser))
                {
                    var categoryUri = ressource.GetValue("category");
                    var thisCategoryId = Umbraco.TypedContent(categoryUri).Id;

                    if(thisCategoryId == categoryId)
                    {
                        RessourceItem usersRessource = new RessourceItem();
                        usersRessource.Name = ressource.Name;
                        usersRessource.Url = ressource.GetValue("file").ToString();
                        usersRessource.Filetype = Path.GetExtension(usersRessource.Url).Replace(".", "");

                        var categoryUdi = ressource.GetValue("category").ToString();
                        var thisCategory = cs.GetById(Umbraco.TypedContent(categoryUdi).Id);

                        CategoryItem category = new CategoryItem();

                        category.Id = thisCategory.Id;
                        category.Name = thisCategory.Name;
                        category.ShortName = thisCategory.GetValue("shortName").ToString();
                        category.StandardMessage = thisCategory.GetValue("standardMessage").ToString();
                        category.isOnlyMessages = thisCategory.GetValue<bool>("isOnlyMessages");
                        var colorString = thisCategory.GetValue("categoryColor").ToString();
                        ColorItem color = JsonConvert.DeserializeObject<ColorItem>(colorString);
                        category.Color = color;

                        usersRessource.Category = category;

                        categoryRessources.Add(usersRessource);
                    }
                }
            }

            if(categoryRessources.Any())
            {
                return Ok(categoryRessources);
            }
            else
            {
                return StatusCode(System.Net.HttpStatusCode.NoContent);
            }

            
        }
    }
}
