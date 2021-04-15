import { HttpClientTestingModule } from '@angular/common/http/testing';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { EventType } from '@app/_models';
import { AccountService, AlertService, EventService } from '@app/_services';
import { of } from 'rxjs';

import { CreateEventComponent } from './create-event.component';

describe('CreateEventComponent', () => {
  let component: CreateEventComponent;
  let fixture: ComponentFixture<CreateEventComponent>;
  let mockEventType: EventType[];
  let formBuilder: FormBuilder;
  let actRoute: ActivatedRoute;
  let router: Router;
  let eventService: EventService;
  let alertService: AlertService;
  let accountService: Partial<AccountService>;

  accountService = {
    account: of({
      id: "1",
      title: "Mr.",
      firstName: "Test",
      lastName: "Testing",
      email: "testing@gmail.com",
      isEventManager: false,
      role: 1,
    })
  };

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule, RouterTestingModule.withRoutes([])],
      declarations: [ CreateEventComponent ],
      providers: [
        {provide: FormBuilder, useValue: formBuilder},
        {provide: ActivatedRoute, useValue: actRoute},
        {provide: Router, useValue: router},
        {provide: EventService, useValue: eventService},
        {provide: AlertService, useValue: alertService},
        {provide: AccountService, useValue: accountService},
      ]
      /*
      formBuilder: FormBuilder,
      private route: ActivatedRoute,
      private router: Router,
      private eventService: EventService,
      private alertService: AlertService,
      private accountService: AccountService
      */
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateEventComponent);
    component = fixture.componentInstance;
    component.account = {
      id: "1",
      title: "Mr.",
      firstName: "Test",
      lastName: "Testing",
      email: "testing@gmail.com",
      isEventManager: false,
      role: 1,
    }

    mockEventType = [{id: "1", name: "test"}, {id: "2", name: "test2"}]
    router = TestBed.inject(Router);
    fixture.detectChanges();
  });

  it('should create', () => {
    const eventSpy = fixture.debugElement.injector.get(EventService);
    const spy = spyOn(eventSpy, 'getEventTypes').and.returnValue(of({id:"1", name:"asdf"}));
    expect(component).toBeTruthy();
  });
});
