namespace Abp.Crud.Models;

public class SelectListModel
{
    public SelectListModel(int? id, string name)
    {
        Id = id;
        Name = name;
    }

    public int? Id { get; set; }
    public string Name { get; set; }
}