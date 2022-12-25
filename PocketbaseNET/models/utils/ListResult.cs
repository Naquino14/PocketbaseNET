using PocketbaseNET.utils;

namespace PocketbaseNET.models.utils
{
    /// <summary>
    /// Used to represent a list of models.
    /// </summary>
    public class ListResult<M> where M : BaseModel
    {
        /// <summary>
        /// The current page of the list.
        /// </summary>
        public int Page { get; private set; }

        /// <summary>
        /// The total number of pages.
        /// </summary>
        public int PerPage { get; private set; }

        /// <summary>
        /// The total items in the list;
        /// </summary>
        public int TotalItems { get; private set; }

        /// <summary>
        /// The total pages in the list.
        /// </summary>
        public int TotalPages { get; private set; }

        /// <summary>
        /// The array of items.
        /// </summary>
        public M[] Items { get; private set; }

        /// <summary>
        /// The indexer of the list.
        /// </summary>
        /// <param name="index">The index to access.</param>
        /// <returns></returns>
        public M this[int index] 
        {
            get => Items[index]; 
            private set => Items[index] = value;
        }

        /// <summary>
        /// Create a new ListResult.
        /// </summary>
        /// <param name="page">The current page number.</param>
        /// <param name="perPage">The number of items per page.</param>
        /// <param name="totalItems">The total number of items in the result.</param>
        /// <param name="totalPages">The total pages in the result.</param>
        /// <param name="items">The array of items in the result.</param>
        public ListResult(int page, int perPage, int totalItems, int totalPages, M[]? items)
        {
            Page = page > 0 ? page : 1;
            PerPage = perPage >= 0 ? PerPage : 0;
            TotalItems = totalItems >= 0 ? totalItems : 0;
            TotalPages = totalPages >= 0 ? totalPages : 0;
            if (items is null)
                Items = Array.Empty<M>();
            else
            {
                Items = new M[items.Length];
                for (int i = 0; i < items.Length; i++)
                    this[i] = (M)Cloner.ReflectiveClone(items[i])!;
            }

        }
    }
}
