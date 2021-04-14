import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { EventService, AlertService, AccountService } from '@app/_services';
import { RawPreviewEvent} from './../../_models/RawPreviewEvent';

@Component({
  selector: 'app-showevents',
  templateUrl: './showevents.component.html',
  styleUrls: ['./showevents.component.less']
})
export class ShoweventsComponent implements OnInit {
  public allRawPreviewEvents =  Array<RawPreviewEvent>();
  
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private eventService: EventService,
    private alertService: AlertService,
    private accountService: AccountService
  ) { }

  ngOnInit(): void {

    this.getAllEvents();
  }

  getAllEvents(){
    this.eventService.getAll().subscribe(x => this.allRawPreviewEvents = x);
    console.log(this.allRawPreviewEvents);
  
  }

}
