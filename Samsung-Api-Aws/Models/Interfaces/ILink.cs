﻿namespace SamsungApiAws.Controllers
{
    public interface ILink : ISoftDeletable
    {
        string Image { get; set; } // link of base64
        string Title { get; set; } 
        string Description { get; set; }
        string Url { get; set; }
    }
}