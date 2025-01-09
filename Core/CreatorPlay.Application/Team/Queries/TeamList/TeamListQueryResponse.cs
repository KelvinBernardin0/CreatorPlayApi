namespace CreatorPlay.Application.Team.Queries.TemList
{




        public class TeamListQueryResponse() {             
                    public int Id { get; set; }
                    public string Name { get; set; }
                    public string Description { get; set; }
                //     public bool IsLeader { get; set; }
                    public string creator { get; set; }
                    public DateTime CreateDate { get; set; }
                    public DateTime? DeactivationDate { get; set; }
                    public string LeaderId { get; set; }
        }
    
}
