import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AccountService, AlertService, EventService } from '@app/_services';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

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
  form: FormGroup;
  loading = false;
  submitted = false;
  esigneduser = false;
  action ="";
  constructor(private route: ActivatedRoute, 
    private eventService: EventService, 
    private accountService: AccountService,
    private alertService: AlertService,
    private router: Router,
    private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.route.queryParams
        .subscribe(params => {
        this.eventid = params.id;
        this.esigneduser = params.signed;
        this.action = params.action;
        console.log(this.eventid);
        console.log(this.action);
      }
    );

    this.form = this.formBuilder.group({
      userid: [this.account.id],
      eventid: [this.eventid],
     
      rating:['', Validators.required],
      description:['',  Validators.required]});

    this.getRawDetailEvent();
  }
  get f() { return this.form.controls; }

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
  onUnRegisterEvent(){
    this.alertService.clear();
   
    this.eventService.unregisterEvent(this.account.id, this.eventid)
      
      .subscribe({
        next: () => {
            console.log("was successful")
            this.alertService.success('Event UnRegisteration successfuly', { keepAfterRouteChange: true });
            this.router.navigate(['../ShowAll'], { relativeTo: this.route });
        },
        error: error => {
            this.alertService.error("I m sorry.. something went wrong...", error);
           // this.router.navigate(['../ShowAll'], { relativeTo: this.route });
        }
    });
  }

  onSubmit() {
    
    this.submitted = true;
   
    // reset alerts on submit
    this.alertService.clear();

    // stop here if form is invalid
    if (this.form.invalid) {
        return;
    }

    this.loading = true;
    
    this.eventService.submitReview(this.form.value)
        .pipe(first())
        .subscribe({
            next: () => {
                this.alertService.success('Review successfuly submitted', { keepAfterRouteChange: true });
                this.router.navigate(['../'], { relativeTo: this.route });
            },
            error: error => {
                this.alertService.error(error);
                this.loading = false;
            }
        });
  }

  isSigned(){
    if ((this.esigneduser) || (this.action=='writeReview'))
      return true;
    else
      return false;
  }
  actionRegisterEvent(){
    if (this.action == 'register')
       return true;
  }
  actionUnRegisterEvent(){
    if (this.action == 'unregister')
       return true;
  }
}


