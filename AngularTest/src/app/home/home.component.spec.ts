// import { async, ComponentFixture, TestBed } from '@angular/core/testing';
// import { AccountService, EventService } from '@app/_services';
// import { of } from 'rxjs';
// import { HomeComponent } from './home.component';

// describe('HomeComponent', () => {
//   let component: HomeComponent;
//   let fixture: ComponentFixture<HomeComponent>;
//   let service: EventService;
//   let service2: AccountService;

//   beforeEach(async(() => {
    
//     TestBed.configureTestingModule({
//       declarations: [ HomeComponent ]
      
//     })
//     .compileComponents();
//   }));

//   beforeEach(() => {
//     service = new EventService(null);
  
//     service2 = new AccountService(null, null);
//     service2.getById('1');
//     component = new HomeComponent(service, service2)
//     // fixture = TestBed.createComponent(HomeComponent);
//     // component = fixture.componentInstance;
//     // fixture.detectChanges();
//   });

//   it('should create', () => {
//     spyOn(service, 'getAllSigned').and.callFake(() => {
//         return of();
//     });
//     component.ngOnInit();
//     expect(component.allSignedEvents).toBeTruthy();
//   });
// });
