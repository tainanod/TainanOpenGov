using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Geo.Grid.Tainan.OpenGov.Dal;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.Enumeration;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;
using Geo.Grid.Tainan.OpenGov.Service.Interface;

namespace Geo.Grid.Tainan.OpenGov.Service
{
    public class CardService : ICardService
    {
        private readonly OpenGovContext _dbModel = new OpenGovContext();

        /// <summary>
        /// Get 九宮格卡片
        /// </summary>
        /// <returns></returns>
        public List<CardListVm> GetApiCards()
        {
            var cards = _dbModel.Card.Where(x => x.IsEnabled&&x.IsPublish).GroupBy(x => x.Color).Select(y => new CardListVm
            {
                Color = y.Key,
                Cards = y.Select(z => new CardVm()
                {
                    CardId = z.CardId,
                    Title = z.Title,
                    Contents = z.Contents,
                    IconId = z.IconId,
                    Color = z.Color,
                    WebUrl = z.WebUrl,
                    Sort = z.Sort,
                    Type = z.Type,
                    IsDelete = z.IsDelete,
                    IsPublish = z.IsPublish,
                }).OrderBy(x => x.Sort).ToList()
            }).OrderBy(x => x.Color).ToList();

            return cards;
        }

        /// <summary>
        /// Get 後台九宮格卡片
        /// </summary>
        /// <returns></returns>
        public List<CardListVm> GetCards()
        {
            var cards = _dbModel.Card.Where(x=>x.IsEnabled).GroupBy(x => x.Color).Select(y => new CardListVm
            {
                Color = y.Key,
                Cards = y.Select(z => new CardVm()
                {
                    CardId = z.CardId,
                    Title = z.Title,
                    Contents = z.Contents,
                    IconId = z.IconId,
                    Color = z.Color,
                    WebUrl = z.WebUrl,
                    Sort = z.Sort,
                    Type = z.Type,
                    IsDelete = z.IsDelete,
                    IsPublish = z.IsPublish,
                }).OrderBy(x => x.Sort).ToList()
            }).OrderBy(x=>x.Color).ToList();

            return cards;

        }

        /// <summary>
        /// 更新卡片列表
        /// </summary>
        /// <param name="cardListVms"></param>
        /// <returns></returns>
        public Result<List<CardListVm>> UpdateCards(List<CardListVm> cardListVms)
        {
            var result = new Result<List<CardListVm>>();

            //取出VM棕色卡片
            //var brownVm = cardListVms.Where(x => x.Color == CardColor.Brown);
            ////記得檢查Null的卡片列表
            //var browns = brownVm.First().Cards;

            ////取出Db棕色卡片
            //var brownsDb = _dbModel.Card.Where(x => x.Color == CardColor.Brown && x.IsEnabled);

            //foreach (var brown in browns)
            //{
            //    var cardId = brown.CardId;
            //    var cardDb = brownsDb.FirstOrDefault(x => x.CardId == cardId);
            //    if (cardDb!=null)
            //    {
            //        cardDb.Sort = browns.FindIndex(x=>x.CardId==cardId);
            //    }
            //}

            //foreach (var cardListVm in cardListVms)
            //{
            //    var cards = cardListVm.Cards;
            //    var cardsDb = _dbModel.Card.Where(x => x.Color == cardListVm.Color && x.IsEnabled);


            //    foreach (var card in cards)
            //    {
            //        var cardId = card.CardId;
            //        var cardDb = cardsDb.FirstOrDefault(x => x.CardId == cardId);
            //        if (cardDb != null)
            //        {
            //            cardDb.Sort = cards.FindIndex(x => x.CardId == cardId);
            //        }
            //    }
            //}

            foreach (var cardListVm in cardListVms)
            {
                var cards = cardListVm.Cards;
                var cardsDb = _dbModel.Card.Where(x => x.Color == cardListVm.Color && x.IsEnabled);

                foreach (var card in cards)
                {
                    var dbCard=_dbModel.Card.First(x=>x.CardId==card.CardId && x.IsEnabled);

                    dbCard.Color = cardListVm.Color;
                    dbCard.Sort= cards.FindIndex(x => x.CardId == card.CardId);
                }
               
            }




            try
            {
                //if (act == null)
                //{
                //    result.Message = "相關連結不存在!";
                //    return result;
                //}
                result.Data = cardListVms;
                result.Success = _dbModel.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 新增卡片
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public Result<CardVm> CreateCard(CardVm card)
        {
            var result = new Result<CardVm>();
            var color = card.Color;
            var sort = this.GetSortByColor(color);

            var data = new Card()
            {
                Title = card.Title,
                IconId = card.IconId,
                Color = card.Color,
                WebUrl = card.WebUrl,
                Sort = sort,
                Type = CardType.其它
            };

            _dbModel.Card.Add(data);

            try
            {
                result.ID = data.CardId;
                result.Data = card;
                result.Success = _dbModel.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        private int GetSortByColor(CardColor color)
        {
            return _dbModel.Card.Count(x => x.Color == color && x.IsEnabled) + 1;
        }

        public Result<CardVm> UpdateContents(CardVm card)
        {
            var result = new Result<CardVm>();
            var cardId = card.CardId;

            var data = _dbModel.Card.FirstOrDefault(x => x.CardId == cardId && x.IsEnabled);

            if (data == null)
            {
                result.Message = "找不到資料";
                return result;
            }
        

            data.Contents = card.Contents;

            try
            {
                result.ID = data.CardId;
                result.Data = card;
                result.Success = _dbModel.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 編輯卡片
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public Result<CardVm> EditCard(CardVm card)
        {
            var result = new Result<CardVm>();
            var cardId = card.CardId;

            var data = _dbModel.Card.FirstOrDefault(x => x.CardId == cardId && x.IsEnabled);

            if (data == null)
            {
                result.Message = "找不到資料";
                return result;
            }


            data.Title = card.Title;
            data.IconId = card.IconId;
            data.WebUrl = card.WebUrl;
            data.IsPublish = card.IsPublish;

            try
            {
                result.ID = data.CardId;
                result.Data = card;
                result.Success = _dbModel.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<string> DeleteCard(Guid cardId)
        {
            var result = new Result<string>();
            

            var data = _dbModel.Card.FirstOrDefault(x => x.CardId == cardId && x.IsEnabled);

            if (data == null)
            {
                result.Message = "找不到資料";
                return result;
            }


            data.IsEnabled = false;
          
            try
            {
                result.ID = data.CardId;
                result.Success = _dbModel.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
    
    }
}
