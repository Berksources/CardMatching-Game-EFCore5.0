using EntityLayer.Result;
using System.Collections.Generic;

namespace WebKartOyunu.Models
{
    public class ScoreTableViewModel
    {
        public ScoreTable ScoreTable { get; set; }
        public IEnumerable<ScoreTable> ScoreTables {get;set;}
        public IEnumerable<ScoreTable> ScoreTableOnly {get;set;}
    }
}
