﻿namespace TodoApi.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<TaskItemTag> TaskItemTags { get; set; } = [];
    }
}
