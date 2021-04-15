import { Component, OnInit } from '@angular/core';
import { EventService } from '@app/_services';
import { RawPreviewEvent} from './../../_models/RawPreviewEvent';

@Component({
  selector: 'app-showevents',
  templateUrl: './showevents.component.html',
  styleUrls: ['./showevents.component.less']
})
export class ShoweventsComponent implements OnInit {
  public buttonArray: any = [];
  public displayEvents=  Array<RawPreviewEvent>();
 

  public allRawPreviewEvents =  Array<RawPreviewEvent>();
  
  constructor(
   
    private eventService: EventService,
  
  ) { }

  ngOnInit(): void {

    this.getAllEvents();
    this.setTable();
    
    
  }

  getAllEvents(){
     this.eventService.getAllUpcoming().subscribe(x => this.allRawPreviewEvents = x);
    console.log(this.allRawPreviewEvents);
  
  }

  setTable(){
    
  let input, filter, table, tr, td, i, txtValue;
  input = document.getElementById("myInput");
  filter = input.value.toUpperCase();
  table = document.getElementById("myTable");
  tr = table.getElementsByTagName("tr");
  for (i = 0; i < tr.length; i++) {
    td = tr[i].getElementsByTagName("td")[0];
    if (td) {
      txtValue = td.textContent || td.innerText;
      if (txtValue.toUpperCase().indexOf(filter) > -1) {
        tr[i].style.display = "";
      } else {
        tr[i].style.display = "none";
      }
    }       
  }
  }

  sortTable(k: string) {
    var j: number = +k
    var table, rows, switching, i, x, y, shouldSwitch;
    table = document.getElementById("myTable");
    switching = true;
    /* Make a loop that will continue until
    no switching has been done: */
    while (switching) {
      // Start by saying: no switching is done:
      switching = false;
      rows = table.rows;
      /* Loop through all table rows (except the
      first, which contains table headers): */
      for (i = 1; i < (rows.length - 1); i++) {
        // Start by saying there should be no switching:
        shouldSwitch = false;
        /* Get the two elements you want to compare,
        one from current row and one from the next: */
        x = rows[i].getElementsByTagName("TD")[j];
        y = rows[i + 1].getElementsByTagName("TD")[j];
        // Check if the two rows should switch place:
        if (x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase()) {
          // If so, mark as a switch and break the loop:
          shouldSwitch = true;
          break;
        }
      }
      if (shouldSwitch) {
        /* If a switch has been marked, make the switch
        and mark that a switch has been done: */
        rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
        switching = true;
      }
    }
  }

  paginateTable(recordPerPage: number){
    let table = document.getElementById("myTable");
    let tr = table.getElementsByTagName("tr");
    let totalRecords = tr.length-1;
    console.log(recordPerPage, totalRecords);
    this.buttonArray = [];
    for (let i =0; i < Math.ceil(totalRecords/recordPerPage); i++){
      console.log('something');
      this.buttonArray.push({i:i+1, recordPerPage: recordPerPage});
      console.log(this.buttonArray[i]);
          
    }
         
  }
  changeRecordPerPage(e){
    console.log(e);
    this.paginateTable(e);
  }

  showPerPage(k, rp){
    console.log("show per page clicked");
    console.log(k, rp);
    rp = parseInt(rp);
    let table, tr, i;
    table = document.getElementById("myTable");
    tr = table.getElementsByTagName("tr");
    console.log(tr.length);
    for (i = 1; i < tr.length; i++) {
      console.log(i,k,rp);

      if (i > (k-1)*rp && i <= (k-1)*rp+rp)
     { 
       
     tr[i].style.display = "";
      } else {
        tr[i].style.display = "none";
      }
    }       
  }

 
}


