import { Component, OnInit } from '@angular/core';
import { first } from "rxjs/operators";

import { AccountService, EventService } from "./../../_services";

@Component({ templateUrl: "elist.component.html" })
export class ElistComponent implements OnInit {
    events: any[];
    account = this.accountService.accountValue;
    gtotal = 0;
    constructor(private accountService: AccountService, private eventService: EventService) {}

    ngOnInit() {
        this.eventService.getAllByManager(this.account.id)
            .pipe(first())
            .subscribe(events => this.events = events);
        this.getAllRevData();
    }

    deleteEvent(id: string) {
        const event = this.events.find(x => x.id === id);
        event.isDeleting = true;
        this.eventService.delete(id)
            .pipe(first())
            .subscribe(() => {
                this.events = this.events.filter(x => x.id !== id) 
            });
    }

    getAllRevData(){
         this.eventService.getAllRevData(this.account.id)
        .pipe(first())
        .subscribe(t => this.gtotal = t);
    }
}