import { HttpClientTestingModule } from '@angular/common/http/testing';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { RawDetailEvent, RawPreviewEvent } from '@app/_models';
import { AccountService, AlertService, EventService } from '@app/_services';
import { Observable, of } from 'rxjs';
import { HomeComponent } from './home.component';

describe('HomeComponent', () => {
  let component: HomeComponent;
  let fixture: ComponentFixture<HomeComponent>;
  let accountServiceStub: Partial<AccountService>;
  let alertServiceStub: AlertService = new AlertService();
  let service: EventService;
  let rawPreviewEvent: RawPreviewEvent;
  let eventServiceStub: EventService;
  let futureEventStub: RawPreviewEvent[];
  let formBuilderStub: FormBuilder = new FormBuilder();
  let service2: AccountService;
  let RouterStub: Router;
  let routerSpy = {navigate: jasmine.createSpy('navigate')};

  class eventStub extends EventService{
    getById(id: string): Observable<RawDetailEvent> { 
      return of ({
        id: '1',
        name: 'Team3',
        date: new Date(),
        location: 'Kansas',
        description: 'Test Event Stub',
        eventType: 'Concert',
        manager: 'Test Manager',
        ticketPrice: 10,
        currentAttending: 300,
        capacity: 300,
        locationMap: 'test location map',          
        });
    }

    registerEvent(userId: string, eventType: string): Observable<string> {
      return of ('successful');
    }
}

  accountServiceStub = {
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

  futureEventStub = [{
      
      id:"c9c2302d",
      name:"Full Seminar Event",
      date:new Date(),
      location:"8040 Independence Pkwy Plano TX 75025",
    
  }];

  beforeEach(async(() => {
    
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule, RouterTestingModule.withRoutes([])],
      declarations: [ HomeComponent ],
      providers: [
        {
          provide: ActivatedRoute, 
          
          useValue: { 
              queryParams: of ({id: 1})
           }

        },
        {
          provide: EventService,
          useClass: eventStub
        },
        {
          provide: AccountService,
          useValue: accountServiceStub
        },
        {
          provide: AlertService,
          useClass: alertServiceStub,
          useValue: {clear: jasmine.createSpy('clear'), success: jasmine.createSpy('success')},
        },
        {
          provide: Router,
          useClass: RouterStub,
          useValue: routerSpy
        },
        {
          provide: FormBuilder,
          useValue: formBuilderStub
        }
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeComponent);
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
    RouterStub = TestBed.inject(Router);
    eventServiceStub = TestBed.inject(EventService);
    fixture.detectChanges();
  });


  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should not call service for future events if account id is null',() => {
    const eventService = fixture.debugElement.injector.get(EventService);
    const spyError = spyOn(eventService, 'getAllFSigned').and.returnValue(of());
    component.account.id= null;
    component. getAllFSignedEvents()
    expect(spyError).toHaveBeenCalled();
  });

  it('should not call service for previous events if account id is null',() => {
    const eventService = fixture.debugElement.injector.get(EventService);
    const spyError = spyOn(eventService, 'getAllPSigned').and.returnValue(of());
    component.account.id= null;
    component. getAllPSignedEvents()
    expect(spyError).toHaveBeenCalled();
  });

  it('should call service for future events if account id is not null',() => {
    const eventService = fixture.debugElement.injector.get(EventService);
    const spyError = spyOn(eventService, 'getAllFSigned').and.returnValue(of(futureEventStub));
    component.account.id= 'asd';
    component. getAllFSignedEvents()
    expect(spyError).toHaveBeenCalled();
  });

  it('should call service for previous events if account id is not null',() => {
    const eventService = fixture.debugElement.injector.get(EventService);
    const spyError = spyOn(eventService, 'getAllPSigned').and.returnValue(of(futureEventStub));
    component.account.id= 'asd';
    component. getAllPSignedEvents()
    expect(spyError).toHaveBeenCalled();
  });


});


