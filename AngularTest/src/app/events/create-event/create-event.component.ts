import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

import { EventService, AlertService, AccountService } from '@app/_services';


@Component({
  selector: 'app-create-event',
  templateUrl: './create-event.component.html',
  styleUrls: ['./create-event.component.less']
})
export class CreateEventComponent implements OnInit {
  account = this.accountService.accountValue;
  form: FormGroup;
  loading = false;
  submitted = false;

  constructor(
      private formBuilder: FormBuilder,
      private route: ActivatedRoute,
      private router: Router,
      private eventService: EventService,
      private alertService: AlertService,
      private accountService: AccountService
  ) { }

  ngOnInit() {
    this.form = this.formBuilder.group({
        managerid: [this.account.id],
        etype: ['', Validators.required],
        name: ['', Validators.required],
        date: ['', Validators.required],
        eventType:"ea3bf8bf-5e12-45b0-bd9a-209defc23e9c",
        street:[''],
        ticketPrice: [''],
        capacity:[''],
        city:[''],
        state:[''],
        zipCode:[''],
        description:[''],

    });
}

  // convenience getter for easy access to form fields
  get f() { return this.form.controls; }

  onSubmit() {
      this.submitted = true;
        
      // reset alerts on submit
      this.alertService.clear();

      // stop here if form is invalid
      if (this.form.invalid) {
          return;
      }

      this.loading = true;
      this.eventService.create(this.form.value)
          .pipe(first())
          .subscribe({
              next: () => {
                  this.alertService.success('Event creation successful, please check your email for verification instructions', { keepAfterRouteChange: true });
                  this.router.navigate(['../events'], { relativeTo: this.route });
              },
              error: error => {
                  this.alertService.error(error);
                  this.loading = false;
              }
          });
  }

}
