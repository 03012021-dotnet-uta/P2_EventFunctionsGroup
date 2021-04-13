import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ShoweventsComponent } from './showevents.component';

describe('ShoweventsComponent', () => {
  let component: ShoweventsComponent;
  let fixture: ComponentFixture<ShoweventsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ShoweventsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ShoweventsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
