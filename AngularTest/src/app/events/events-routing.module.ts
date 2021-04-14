import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CreateEventComponent } from './create-event/create-event.component';
import { LayoutComponent } from './layout.component';
import { ShoweventsComponent} from './showevents/showevents.component';
import { DetaileventComponent} from './detailevent/detailevent.component';

const routes: Routes = [
    {
        path: '', component: LayoutComponent,
        children: [
            { path: 'Create', component: CreateEventComponent },
            { path: 'ShowAll', component: ShoweventsComponent },
            { path: 'Details', component: DetaileventComponent },
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class EventsRoutingModule { }