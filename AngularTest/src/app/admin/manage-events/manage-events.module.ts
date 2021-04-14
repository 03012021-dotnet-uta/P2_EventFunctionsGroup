import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { ManageEventsRoutingModule } from './manage-events-routing.module';
import { ElistComponent } from './elist.component';
import { AddEditComponent } from './add-edit.component';

@NgModule({
    imports: [
        CommonModule,
        ReactiveFormsModule,
        ManageEventsRoutingModule
    ],
    declarations: [
        ElistComponent,
        AddEditComponent
    ]
})
export class ManageEventsModule { }