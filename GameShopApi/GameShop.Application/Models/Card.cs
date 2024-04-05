using GameShop.Application.Models.Scryfall;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using System.Xml.Linq;

namespace GameShop.Application.Models;

public class Card
{
    public Guid Id { get; set; }
    public Guid ExternalId { get; set; }
    public Guid OracleId { get; set; }
    public bool? Promo { get; set; }
    public double? ManaValue { get; set; }
    public string? Artist { get; set; }
    public string? CollectorNumber { get; set; }
    public string Name { get; set; }
    public string? ExternalUri { get; set; }
    public string? ExternalSource { get; set; }
    public string? SetCode { get; set; }
    public string? SetName { get; set; }

    /// <summary>
    /// Includes super type and sub type of a card
    /// eg: "Legendary Artifact Creature — Golem", or "Instant", or "Artifact"
    /// </summary>
    public string? TypeLine { get; set; }
    public FoilType? FoilType { get; set; }
    public LayoutType? Layout { get; set; }
    public RarityType? Rarity { get; set; }
    public ImageUris ImageUris { get; set; }
    public Prices Prices { get; set; }
    public List<string>? ColorIdentity { get; set; }
    public List<string>? Colors { get; set; }
    public List<FoilType>? AvailableFinishes { get; set; }
}

public class ProductBase
{
    // TODO: Implement base product class

    public Guid Id { get; set; }
    public DateTime? CreatedDate { get; set; }
    public Guid? CreatedBy { get; set; }
    public Guid? ModifiedBy { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ThumbnailImageUri { get; set; }
    public string DisplayImageUri { get; set; }
    public string Description { get; set; }
    public string Description { get; set; }
    public string Description { get; set; }

    //   Name
    //   Description

    //   ThumbnailImage
    //   DisplayImage

    //   List of images
    //   SellPrice

    //   WholesalePrice(Purchase price)

    //   Sale%
    //   SaleFlat
    //   SalePrice

    //   Stock
}

public class ImageUris
{
    public string? ArtCrop { get; set; }
    public string? BorderCrop { get; set; }
    public string? Large { get; set; }
    public string? Normal { get; set; }
    public string? Small { get; set; }
}

public class Prices
{
    public string? Usd { get; set; }
    public string? UsdEtched { get; set; }
    public string? UsdFoil { get; set; }
}

public enum LayoutType
{
    Normal = 0,
    Transform = 1,
}

public enum RarityType
{
    Common = 0,
    Uncommon = 1,
    Rare = 2,
    Mythic = 3,
}

public enum FoilType
{
    Nonfoil = 0,
    Foil = 1,
}