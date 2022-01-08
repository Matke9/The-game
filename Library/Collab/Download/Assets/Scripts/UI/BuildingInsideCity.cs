using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInsideCity
{
    public bool Bought = false;
    public int PriceGold;
    public int PriceWood;
    public int PriceStone;
    public int PriceIron;
    public int PriceCrystals;
    public Unit SellingUnit;
    public double Xcoord;
    public double Ycoord;

    public BuildingInsideCity(int pg, int pw, int ps, int pi, int pc, Unit u, double x, double y)
    {
        this.PriceGold = pg;
        this.PriceWood = pw;
        this.PriceStone = ps;
        this.PriceIron = pi;
        this.PriceCrystals = pc;
        this.SellingUnit = u;
        this.Xcoord = x;
        this.Ycoord = y;
    }
    public BuildingInsideCity(int pg, int pw, int ps, int pi, int pc, double x, double y)
    {
        this.PriceGold = pg;
        this.PriceWood = pw;
        this.PriceStone = ps;
        this.PriceIron = pi;
        this.PriceCrystals = pc;
        this.Xcoord = x;
        this.Ycoord = y;
    }

}
