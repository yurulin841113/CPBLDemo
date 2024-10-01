using System;
using System.Collections.Generic;

namespace CPBLDemo.Models;

public partial class PlayerList
{
    public int Id { get; set; }

    public int Number { get; set; }

    public string Position { get; set; } = null!;

    public string Name { get; set; } = null!;

    public decimal Height { get; set; }

    public decimal Weight { get; set; }

    public DateTime FirstDate { get; set; }

    public DateTime CreatedTime { get; set; }
}
