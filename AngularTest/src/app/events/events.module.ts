import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateEventComponent } from './create-event/create-event.component';
import { ReactiveFormsModule } from '@angular/forms';
import { EventsRoutingModule } from './events-routing.module';
import { LayoutComponent } from './layout.component';
import { ShoweventsComponent } from './showevents/showevents.component';
import { DetaileventComponent } from './detailevent/detailevent.component';


@NgModule({
  declarations: [CreateEventComponent, LayoutComponent, ShoweventsComponent, DetaileventComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    EventsRoutingModule,
    
  ]
})
export class EventsModule { }
