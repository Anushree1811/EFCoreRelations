﻿namespace EFCoreRelations.Entity;

public class User
{

    public int Id { get; set; }

    public string UserName { get; set; }

    public List<Character> Character { get; set; }
}
