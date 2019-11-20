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
        public List<RessourceItem> GetAllRessources()
        {
            var currentUser = Members.CurrentUserName;
            IMemberService ms = Services.MemberService;
            IContentService cs = Services.ContentService;

            var everyRessource = cs.GetContentOfContentType(1100);

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

            return usersRessources;
        }

        [RoleAuthorize]
        [HttpGet]
        public List<RessourceItem> GetRessourcesForCategory(int categoryId)
        {
            var currentUser = Members.CurrentUserName;
            IContentService cs = Services.ContentService;

            var everyRessource = cs.GetContentOfContentType(1100);

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
                        categoryRessources.Add(usersRessource);
                    }
                }
            }

            return categoryRessources;
        }
    }
}
