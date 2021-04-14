import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map, finalize } from 'rxjs/operators';

import { environment } from '@environments/environment';
import { Event, RawPreviewEvent, RawDetailEvent } from '@app/_models';


const baseUrl = `${environment.apiUrl}/Events`;

@Injectable({ providedIn: 'root' })
export class EventService {
    private EventSubject: BehaviorSubject<Event>;
    public Event: Observable<Event>;
    public RawPreviewEvent: Observable<RawPreviewEvent>;
    public RawDetailEvent: Observable<RawDetailEvent>;

    constructor(
        private router: Router,
        private http: HttpClient
    ) {
        this.EventSubject = new BehaviorSubject<Event>(null);
        this.Event = this.EventSubject.asObservable();
    }

    public get EventValue(): Event {
        return this.EventSubject.value;
    }

   
    create(Event: Event) {
        return this.http.post(`https://eventfunctionsp2.azurewebsites.net/api/Manager/createevent`, Event);
    }

    

    getAll() {
        return  this.http.get<RawPreviewEvent[]>('https://eventfunctionsp2.azurewebsites.net/api/Event/all');       
    }

    getById(id: string) {
        return this.http.get<RawDetailEvent>(`https://eventfunctionsp2.azurewebsites.net/api/Event/eventdetail/${id}`);
    }
    
    
    
    update(id, params) {
        return this.http.put(`${baseUrl}/${id}`, params)
            .pipe(map((Event: any) => {
                // update the current Event if it was updated
                if (Event.id === this.EventValue.id) {
                    // publish updated Event to subscribers
                    Event = { ...this.EventValue, ...Event };
                    this.EventSubject.next(Event);
                }
                return Event;
            }));
    }
    
    delete(id: string) {
        return this.http.delete(`${baseUrl}/${id}`)
            .pipe(finalize(() => {
                // auto logout if the logged in Event was deleted
                if (id === this.EventValue.id)
                    this.getAll();
            }));
    }

    registerEvent(uid: string, eid: string){
        return this.http.get(`https://eventfunctionsp2.azurewebsites.net/api/Event/signup/${uid}/${eid}`);
    }

    
}