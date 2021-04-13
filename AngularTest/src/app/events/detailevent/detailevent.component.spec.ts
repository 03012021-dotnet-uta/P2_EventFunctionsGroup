import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { Router, ActivatedRoute } from '@angular/router';
import { AlertType, RawDetailEvent } from '@app/_models';
import { AccountService, AlertService, EventService } from '@app/_services';
import { Observable, of } from 'rxjs';
import { DetaileventComponent } from './detailevent.component';


describe('DetaileventComponent', () => {
  let component: DetaileventComponent;
  let fixture: ComponentFixture<DetaileventComponent>;
  let spy: any;
  let testAlert; 

  class eventStub {
      getById(id: string): Observable<RawDetailEvent> { 
        return of ({
          id: '1',
          name: 'Team3',
          date: new Date(),
          location: 'Kansas',
          description: 'Test Event Stub',
          eventType: 'Concert',
          manager: 'Test Manager',
          currentAttending: 300,
          capacity: 300,
          locationMap: 'test location map',          
          });
      }

      onRegisterEvent(userId: string, eventType: string): Observable<string> {
        return of ('successful');
      }



      // registerEvent(userId: string, eventType: string): Observable<string> {
      //   return of ('successful');
      // }
  }


  beforeEach(async(() => {
    TestBed.configureTestingModule({
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
          useValue: { AccountService }
        },
        {
          provide: AlertService,
          useValue: { AlertService }
        },
        {
          provide: Router,
          useValue: { Router }
        }

      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DetaileventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();

    

  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should register', () => {
    testAlert = new AlertService();
    spy = spyOn(testAlert, 'clear').withArgs('default-alert');
    expect(component.onRegisterEvent()).toBeTruthy();
  })
});


