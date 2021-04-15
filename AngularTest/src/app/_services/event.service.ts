import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map, finalize } from 'rxjs/operators';

import { environment } from '@environments/environment';
import { Event, RawPreviewEvent, RawDetailEvent, Review, Reviews, RawManagerEvent, EventType } from '@app/_models';


const baseUrl = `${environment.apiUrl}/Events`;

@Injectable({ providedIn: 'root' })
export class EventService {
    private EventSubject: BehaviorSubject<Event>;
    public Event: Observable<Event>;
    public RawPreviewEvent: Observable<RawPreviewEvent>;
    public RawDetailEvent: Observable<RawDetailEvent>;
    public Review: Observable<Review>;
    public Reviews: Observable<Reviews>;
    public RawManagerEvent: Observable<RawManagerEvent>;
    public EventType: Observable<EventType>;

    constructor(
      //  private router: Router,
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
    getAllUpcoming() {
        return  this.http.get<RawPreviewEvent[]>('https://eventfunctionsp2.azurewebsites.net/api/Event/allupcoming');       
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
        return this.http.delete(`https://eventfunctionsp2.azurewebsites.net/api/Manager/delete/${id}`)
            .pipe(finalize(() => {
                // auto logout if the logged in Event was deleted
                if (id === this.EventValue.id)
                    this.getAll();
            }));
    }


    registerEvent(uid: string, eid: string){
        return this.http.get(`https://eventfunctionsp2.azurewebsites.net/api/Event/signup/${uid}/${eid}`);
    }
    unregisterEvent(uid: string, eid: string){
        return this.http.delete(`https://eventfunctionsp2.azurewebsites.net/api/Event/deletesignup/${uid}/${eid}`);
    }
    getAllFSigned(uid: string){
        return this.http.get<RawPreviewEvent[]>(`https://eventfunctionsp2.azurewebsites.net/api/Event/allsigned/${uid}`);
    }

    getAllPSigned(uid: string){
        return this.http.get<RawPreviewEvent[]>(`https://eventfunctionsp2.azurewebsites.net/api/Event/allprevious/${uid}`);
    }

    submitReview(review: Review){
        return this.http.post(`https://eventfunctionsp2.azurewebsites.net/api/Event/review`, review);
    }

    getAllReviews(eid: string){
        return this.http.get<Reviews[]>(`https://eventfunctionsp2.azurewebsites.net/api/Event/allreviews/${eid}`);
    }

    getAllByManager(uid: string){
        return this.http.get<RawManagerEvent[]>(`https://eventfunctionsp2.azurewebsites.net/api/Manager/getevents/${uid}`);
    }
    getAllRevData(uid: string){
        return this.http.get<number>(`https://eventfunctionsp2.azurewebsites.net/api/Manager/getallrevdata/${uid}`);
    }

    getEventTypes(){
        return this.http.get<EventType>(`https://eventfunctionsp2.azurewebsites.net/api/Manager/eventtypes`)
    }
}