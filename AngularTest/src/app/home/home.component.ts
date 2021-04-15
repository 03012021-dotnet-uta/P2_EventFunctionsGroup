import { Component, OnInit } from '@angular/core';

import { EventService,  AccountService } from '@app/_services';
import { RawPreviewEvent} from './../_models';

@Component({ templateUrl: 'home.component.html' })
export class HomeComponent {
    account = this.accountService.accountValue;
    public allFSignedEvents =  Array<RawPreviewEvent>();
    public allPSignedEvents =  Array<RawPreviewEvent>();

    constructor(
    
      private eventService: EventService,
    
      private accountService: AccountService
    ) { } 

    ngOnInit(): void {

      this.getAllFSignedEvents();
      this.getAllPSignedEvents();
    }
  
    getAllFSignedEvents(){
      if(this.account.id)
      { 
      this.eventService.getAllFSigned(this.account.id)
      .subscribe(x => this.allFSignedEvents = x);
      console.log(this.allFSignedEvents);
      }
    
    }

    getAllPSignedEvents(){
      if(this.account.id)
      { 
      this.eventService.getAllPSigned(this.account.id)
      .subscribe(x => this.allPSignedEvents = x);
      console.log(this.allPSignedEvents);
      }
    }
}