using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;

namespace Geo.Grid.Tainan.OpenGov.Service.Interface
{
    public interface ICardService
    {
        /// <summary>
        /// Get 後台九宮格卡片
        /// </summary>
        /// <returns></returns>
        List<CardListVm> GetCards();


        /// <summary>
        /// Get 九宮格卡片
        /// </summary>
        /// <returns></returns>
        List<CardListVm> GetApiCards();

        /// <summary>
        /// 更新卡片列表
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        Result<List<CardListVm>> UpdateCards(List<CardListVm> cards);


        /// <summary>
        /// 新增卡片
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        Result<CardVm> CreateCard(CardVm card);

        /// <summary>
        /// 編輯卡片
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        Result<CardVm> EditCard(CardVm card);

        /// <summary>
        /// 刪除卡片
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        Result<string> DeleteCard(Guid cardId);

        /// <summary>
        /// 更新卡片內文
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        Result<CardVm> UpdateContents(CardVm card);
    }
}
