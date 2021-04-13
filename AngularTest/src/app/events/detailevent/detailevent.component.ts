import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AccountService, AlertService, EventService } from '@app/_services';

import { RawDetailEvent} from './../../_models';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-detailevent',
  templateUrl: './detailevent.component.html',
  styleUrls: ['./detailevent.component.less']
})
export class DetaileventComponent implements OnInit {
  eventid: string;
  revent = new RawDetailEvent();
  account = this.accountService.accountValue;
  constructor(private route: ActivatedRoute, 
    private eventService: EventService, 
    private accountService: AccountService,
    private alertService: AlertService,
    private router: Router) { }

  ngOnInit() {
    this.route.queryParams
        .subscribe(params => {
        this.eventid = params.id;
        console.log(this.eventid);
      }
    );
    this.getRawDetailEvent();
  }

  getRawDetailEvent(){
    this.eventService.getById(this.eventid)
    .subscribe(x=> this.revent = x);
    console.log("hello");
  }

  onRegisterEvent(){
    this.alertService.clear();
   
    this.eventService.registerEvent(this.account.id, this.eventid)
      
      .subscribe({
        next: () => {
            console.log("was successful")
            this.alertService.success('Event Registeration successful, please check your email for verification instructions', { keepAfterRouteChange: true });
            this.router.navigate(['../ShowAll'], { relativeTo: this.route });
        },
        error: error => {
            this.alertService.error("I m sorry.. something went wrong...", error);
           // this.router.navigate(['../ShowAll'], { relativeTo: this.route });
        }
    });
  }
}


