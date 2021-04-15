import { HttpClientTestingModule } from '@angular/common/http/testing';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ActivatedRoute } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { Review, Reviews } from '@app/_models';
import { EventService } from '@app/_services';
import { of } from 'rxjs';

import { ReviewsComponent } from './reviews.component';

describe('ReviewsComponent', () => {
  let component: ReviewsComponent;
  let fixture: ComponentFixture<ReviewsComponent>;
  let fakeReviews: Reviews[];
  let actRoute: Partial<ActivatedRoute>;

  class eventService extends EventService{

  }

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule, RouterTestingModule.withRoutes([])],
      declarations: [ ReviewsComponent ],
      providers: [
        {provide: ActivatedRoute, useValue: actRoute},
        {provide: EventService, useclass: eventService},
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReviewsComponent);
    component = fixture.componentInstance;
    fakeReviews = [{user: "1", event: "2", rating: "3", description: "Test"}];
    component.id = "1";
    fixture.detectChanges();
  });

  it('should create', () => {
    const eventService = fixture.debugElement.injector.get(EventService);
    const spy = spyOn(eventService, 'getAllReviews').and.returnValue(of(fakeReviews));
    expect(component).toBeTruthy();
  });
});
