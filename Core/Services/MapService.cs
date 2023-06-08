using Contracts;
using Contracts.Models;
using Data.Models;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services;
public class MapService : IMapService
{
    private readonly IUnitOfWork _unitOfWork;
    public MapService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async void CreateRandomMap()
    {
        int countPlayers = 2;

        var map = new Map();
        map.Id = Guid.NewGuid();
        map.Players = countPlayers;
        var allMaps = await _unitOfWork.MapRepository.GetAsync();
        map.Name = $"Random map  {allMaps.Count()}";

        var lands = await _unitOfWork.LandRepository.GetAsync();
        Console.WriteLine("Land list:");
        foreach (var u in lands)
        {
            Console.WriteLine($"{u.Id}.{u.Name} - {u.IsPassable}, {u.Emoji}");
        }
        var landList = lands.ToList();
        var resources = await _unitOfWork.ResourceRepository.GetAsync();
        Console.WriteLine("Resource list:");
        foreach (var u in resources)
        {
            Console.WriteLine($"{u.Id} - {u.Name} - {u.Emoji}");
        }
        var resourcesList = resources.ToList();
        var rnd = new Random();
        Console.WriteLine();
        Console.WriteLine("Map:");
        //for (int i = 0; i < map.SizeX; i++)
        //{
        //    for (int j = 0; j < map.SizeY; j++)
        //    {
        //        var randomLandID = rnd.Next(landList.Count);
        //        var randomResourceID = rnd.Next(resourcesList.Count);
        //        var random10 = rnd.Next(10);
        //        var cell = new CellEntity();
        //        var resource = null;
        //        cell.CoordinateX = i;
        //        cell.CoordinateY = j;
        //        var land = landList[randomLandID];
        //        var thing = new Thing();
        //        cell.Id = Guid.NewGuid();
        //        cell.LandId = land.Id;
        //        //cell.IsOpen = false;
        //        cell.MapID = map.Id;
        //        if (i == 0 && j == 0)
        //            cell.PlayerPosition = 1;
        //        else if (i == map.SizeX - 1 && j == map.SizeY - 1)
        //            cell.PlayerPosition = 2;
        //        if (random10 == 0 && land.Passability == Passabilities.Possible)
        //        {
        //            resource = resourcesList[randomResourceID];
        //            thing.Id = Guid.NewGuid();
        //            thing.ThingType = "Resource";
        //            thing.Name = resource.Name;
        //            thing.ResourceId = resource.Id;
        //            //thing.ThingId = resource.Id;
        //            thing.Emoji = resource.Emoji;
        //            thingRepository.Create(thing);
        //            thingRepository.Save();
        //            thing.Resource = resource;
        //        }
        //        else
        //        {
        //            thing = Creator.emptyThing;
        //        }
        //        cell.ThingId = thing.Id;
        //        cellRepository.Create(cell);
        //        cellRepository.Save();
        //        cell.Map = map;
        //        cell.Thing = thing;
        //        cell.Land = landList[randomLandID];
        //        map.Cells[i, j] = cell;
        //    }

        //}


        //map.Print("land");
        //map.Print("thing");
    }

}
