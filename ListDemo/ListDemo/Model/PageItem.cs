// ***********************************************************************
// FileName:PageItem
// Description:
// Project:
// Author:NewBLife
// Created:2016/8/28 16:47:59
// Copyright (c) 2016 NewBLife,All rights reserved.
// ***********************************************************************

using System.Collections.Generic;

namespace ListDemo.Model
{
    public class PageItem
    {
        public string Title { get; set; }

        public string ImgUrl { get; set; }
    }

    public class PageList
    {
        public static List<PageItem> GetData()
        {
            return new List<PageItem>() {
                new PageItem() { Title="图1",ImgUrl="ms-appx:///Assets/imgs/1 (1).jpg"},
                new PageItem() { Title="图2",ImgUrl="ms-appx:///Assets/imgs/1 (2).jpg"},
                new PageItem() { Title="图3",ImgUrl="ms-appx:///Assets/imgs/1 (3).jpg"},
                new PageItem() { Title="图4",ImgUrl="ms-appx:///Assets/imgs/1 (3).jpg"},
                new PageItem() { Title="图5",ImgUrl="ms-appx:///Assets/imgs/1 (3).jpg"},
                new PageItem() { Title="图6",ImgUrl="ms-appx:///Assets/imgs/1 (3).jpg"},
                new PageItem() { Title="图7",ImgUrl="ms-appx:///Assets/imgs/1 (3).jpg"},
                new PageItem() { Title="图8",ImgUrl="ms-appx:///Assets/imgs/1 (3).jpg"},
                new PageItem() { Title="图9",ImgUrl="ms-appx:///Assets/imgs/1 (3).jpg"},
                new PageItem() { Title="图10",ImgUrl="ms-appx:///Assets/imgs/1 (3).jpg"},
                new PageItem() { Title="图11",ImgUrl="ms-appx:///Assets/imgs/1 (3).jpg"},
                new PageItem() { Title="图12",ImgUrl="ms-appx:///Assets/imgs/1 (3).jpg"},
                new PageItem() { Title="图13",ImgUrl="ms-appx:///Assets/imgs/1 (3).jpg"},
                new PageItem() { Title="图14",ImgUrl="ms-appx:///Assets/imgs/1 (3).jpg"},
                new PageItem() { Title="图15",ImgUrl="ms-appx:///Assets/imgs/1 (3).jpg"}
            };
        }
    }
}
