using System.Collections.Generic;

namespace FrontToBack103.ViewModels
{
    public class Pagination<T>
    {
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
        public List<T> Items { get; set; }

        public Pagination(int pageCount,int currentPage,List<T>items)
        {
            PageCount=pageCount;
            CurrentPage=currentPage;
            Items=items;

        }
    }
}
