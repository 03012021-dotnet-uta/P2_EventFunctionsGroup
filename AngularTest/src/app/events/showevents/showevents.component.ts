import { Component, OnInit } from '@angular/core';
import { EventService } from '@app/_services';
import { RawPreviewEvent} from './../../_models/RawPreviewEvent';

@Component({
  selector: 'app-showevents',
  templateUrl: './showevents.component.html',
  styleUrls: ['./showevents.component.less']
})
export class ShoweventsComponent implements OnInit {
  public allRawPreviewEvents =  Array<RawPreviewEvent>();
  
  constructor(
   
    private eventService: EventService,
  
  ) { }

  ngOnInit(): void {

    this.getAllEvents();
  }

  getAllEvents(){
    this.eventService.getAllUpcoming().subscribe(x => this.allRawPreviewEvents = x);
    console.log(this.allRawPreviewEvents);
  
  }

}
