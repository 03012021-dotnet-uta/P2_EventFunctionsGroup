import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Account, AlertType, RawDetailEvent } from '@app/_models';
import { AccountService, AlertService, EventService } from '@app/_services';
import { Observable, of, throwError } from 'rxjs';
import { DetaileventComponent } from './detailevent.component';
import { HttpClientTestingModule } from '@angular/common/http/testing'
import { RouterTestingModule } from '@angular/router/testing'
import { stringify } from '@angular/compiler/src/util';
import { HttpClient } from '@angular/common/http';


describe('DetaileventComponent', () => {
  let component: DetaileventComponent;
  let fixture: ComponentFixture<DetaileventComponent>;
  let accountServiceStub: Partial<AccountService>;
  let formBuilderStub: FormBuilder = new FormBuilder();
  let alertServiceStub: AlertService = new AlertService();
  let eventServiceStub: EventService;
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


  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule, RouterTestingModule.withRoutes([])],
      declarations: [ DetaileventComponent ],
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
    fixture = TestBed.createComponent(DetaileventComponent);
    component = fixture.componentInstance;
    component.form = formBuilderStub.group({
      userid: "1",
      eventid: "1",
      rating: 3,
      description: "This is a test"
    });
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

  it('should register successfully', () => {
    const eventService = fixture.debugElement.injector.get(EventService);
    const spyError = spyOn(eventService, 'registerEvent').and.returnValue(of(true));
    component.onRegisterEvent();
    expect(routerSpy.navigate).toHaveBeenCalled();
  })

  // it('should register unsuccessfully', () => {
  //   const eventService = fixture.debugElement.injector.get(EventService);
  //   const spyError = spyOn(eventService, 'registerEvent').and.returnValue(throwError  ({status: 404}));
  //   component.onRegisterEvent();
  //   expect(routerSpy.navigate).not.toHaveBeenCalled();
  // })

  it('should unregister successfully', () => {
    const eventService = fixture.debugElement.injector.get(EventService);
    const spyError = spyOn(eventService, 'unregisterEvent').and.returnValue(of(true));
    component.onUnRegisterEvent();
    expect(routerSpy.navigate).toHaveBeenCalled();
  })

  it('should submit review', () => {
    const eventService = fixture.debugElement.injector.get(EventService);
    const spyError = spyOn(eventService, 'submitReview').and.returnValue(of(true));
    component.onSubmit();
    expect(routerSpy.navigate).toHaveBeenCalled();
  })

  it('should is signed true', () => {
    component.esigneduser = true;
    fixture.detectChanges();
    let returnValue = component.isSigned();
    expect(returnValue).toEqual(true);
  })

  it('should is signed false', () => {
    component.esigneduser = false;
    fixture.detectChanges();
    let returnValue = component.isSigned();
    expect(returnValue).toEqual(false);
  })

  it('should actionRegisterEvent', () => {
    component.action = 'register';
    fixture.detectChanges();
    let returnValue = component.actionRegisterEvent();
    expect(returnValue).toEqual(true);
  })

  it('should actionUnRegisterEvent', () => {
    component.action = 'unregister';
    fixture.detectChanges();
    let returnValue = component.actionUnRegisterEvent();
    expect(returnValue).toEqual(true);
  })
});


