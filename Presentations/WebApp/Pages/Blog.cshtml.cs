using AutoMapper;
using BusinessLayer.Services;
using EntityLayer.DTOs;
using WebApp.Pages.Shared;

namespace WebApp.Pages
{
    public class BlogModel : _BasePageModel
    {

        public static List<GenericValueDTO> blogDatas = new List<GenericValueDTO>();
        public List<GenericValueDTO> blogList = new List<GenericValueDTO>();
        public List<GenericValueDTO> blogListByCategory = new List<GenericValueDTO>();
        public int activePage = 1;
        public int totalPage = 1;
        public int blogCountByCategory = 0;
        public int categoryId = 0;
        public BlogModel(IMapper mapper, GenericValueService genericValueService) : base(mapper, genericValueService)
        {
        }

        public void OnGet(string category, int p = 1)
        {

            #region eski kod 
            //int rowCount = 21;
            //totalPage = (blogs.Count() % rowCount) == 0 ? blogs.Count() / rowCount : (blogs.Count() / rowCount) + 1;

            //activePage = p;
            //blogList = blogs.Skip((p - 1) * rowCount).Take(rowCount).ToList();
            #endregion

            if (blogDatas.Count==0)
            {
                 blogDatas = _genericValueService.GetList(0, 10000, "", 15).Data;
            }
            

            #region yeni kod 

            int rowCount = 21;

            activePage = p;

            blogListByCategory = _genericValueService.GetBlogCategoryList().Data;

            if (category != null) // seçili kategoriye ait bloglar gelsin
            {
                categoryId = blogListByCategory.FirstOrDefault(x => x.seo_url == category).id;

                blogList = blogDatas.Skip((p - 1) * rowCount).Take(rowCount).Where(x => x.parent_1_id == categoryId).ToList();
                if (blogList.Count<21 && p==1)
                {
                    totalPage = 1;
                }
                else
                {
                    totalPage =
                    (blogDatas.Where(x => x.parent_1_id == categoryId).Count() % rowCount) == 0 ?
                    (blogDatas.Where(x => x.parent_1_id == categoryId).Count() / rowCount) :
                    (blogDatas.Where(x => x.parent_1_id == categoryId).Count() / rowCount) + 1; //12
                }

            }

            else // tüm bloglar gelsin
            {
                blogList = blogDatas.Skip((p - 1) * rowCount).Take(rowCount).ToList(); //21
                totalPage = (blogDatas.Count() % rowCount) == 0 ? blogDatas.Count() / rowCount : (blogDatas.Count() / rowCount) + 1; //12

            }
            #endregion
        }
    }
}
