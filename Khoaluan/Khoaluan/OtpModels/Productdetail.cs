using Khoaluan.Models;
using Khoaluan.ModelViews;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Khoaluan.OtpModels
{
    public class Productdetail
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Overview { get; set; }    
        public string Price { get; set; }
        public string Image { get; set; }   
        public string Description { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ReleaseDate { get; set; }
        public List<string> Categories { get; set; }
        public string DevName { get; set; }
        public string CatID { get; set; }
    }
    public class ProductDetailModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }
        public string Price { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ReleaseDate { get; set; }
        public string Category { get; set; }
        public string DevName { get; set; }
        public string CatID { get; set; }
    }
    public class LibraryDetail
    {
        public int UserID { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
    public class HomePageViewModel
    {
        public List<Product> bestSeller { get; set; }
        public List<Product> FreeGames { get; set; } 
        public List<Productdetail> PopularGame { get; set; }
        public List<Productdetail> RecentlyRealeased { get; set; }
        public List<Category> cate { get; set; }
        public List<Category> catesecond { get; set; }
        public Productdetail detail { get; set; }
    }
    public class DetailPage
    {
        public Productdetail productDetail { get; set; }   
        public List<Productdetail> relateGame { get; set; }
        public List<Productdetail> popularGame { get; set; }
        public List<Category> cate { get; set; }
        public AddToCart add { get; set; }
    }
    public class DetailCate
    {
        public List<Productdetail> productwithCate { get; set; }
        public List<Productdetail> PopularGame1 { get; set; }
    }
}
