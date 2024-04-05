using AutoMapper;
using GameShop.Application.Models;
using GameShop.Application.Models.Scryfall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShop.Application.Mappers;
public class MainMapper : Profile
{
    public MainMapper()
    {
        CreateMap<PostCard_Response, Card>();
    }
}
