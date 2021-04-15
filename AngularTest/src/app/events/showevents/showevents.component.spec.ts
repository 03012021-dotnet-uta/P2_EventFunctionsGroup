import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ɵgetComponentViewDefinitionFactory } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { RawPreviewEvent } from '@app/_models';
import { EventService } from '@app/_services';
import { of } from 'rxjs';

import { ShoweventsComponent } from './showevents.component';

describe('ShoweventsComponent', () => {
  let component: ShoweventsComponent;
  let fixture: ComponentFixture<ShoweventsComponent>;
  let eventService: EventService;
  let mockRawEvents: RawPreviewEvent[];

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule, RouterTestingModule.withRoutes([])],
      declarations: [ ShoweventsComponent ],
      providers: [
        {
          provide: EventService,
          useClass: eventService
        }]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ShoweventsComponent);
    component = fixture.componentInstance;
    mockRawEvents = [{id: "1", name: "AStart", date: new Date(), location: "999 End"},
                      {id: "2", name: "ZEnd", date: new Date(), location: "111 Start"}];
    
    component.allRawPreviewEvents = mockRawEvents;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should get all events', () => {
    const testSpy = fixture.debugElement.injector.get(EventService);
    const spy = spyOn(testSpy, 'getAllUpcoming').and.returnValue(of(mockRawEvents));
    component.getAllEvents();
    expect(spy).toHaveBeenCalled();
  })

  it('should show all events', () => {
    component.setTable();
    let table = document.getElementById("myTable");
    let tr = table.getElementsByTagName("tr");
    expect(tr.length - 1).toEqual(component.allRawPreviewEvents.length);
  })

  it('should sort all events by location', () => {
    component.sortTable("0");
    let table = document.getElementById("myTable");
    let tr = table.getElementsByTagName("tr");
    let td = tr[1].getElementsByTagName("td")[0];
    expect(td.innerText).toEqual("AStart");
    component.sortTable("1")
    table = document.getElementById("myTable");
    tr = table.getElementsByTagName("tr");
    td = tr[1].getElementsByTagName("td")[0];
    expect(td.innerText).toEqual("ZEnd");
  })

  it('should display correct number of paginate buttons', () => {
    component.paginateTable(1);
    expect(component.buttonArray.length).toEqual(component.allRawPreviewEvents.length);
    component.paginateTable(2);
    expect(component.buttonArray.length).toEqual(component.allRawPreviewEvents.length/2)
  })

  it('it should call paginateTable', () => {
    const spy = spyOn(component, 'paginateTable');
    component.changeRecordPerPage(1);
    expect(spy).toHaveBeenCalledWith(1);
  })

  it('should display correct number of events per page', () => {
      component.showPerPage(1,1);
      let table = document.getElementById("myTable");
      let tr = table.getElementsByTagName("tr");
      let totalHidden = [].slice.call(tr).filter(function(x){return getComputedStyle(x).display === "none"})
      expect(totalHidden.length).toEqual(1);
      component.showPerPage(1,2);
      table = document.getElementById("myTable");
      tr = table.getElementsByTagName("tr");
      totalHidden = [].slice.call(tr).filter(function(x){return getComputedStyle(x).display === "none"})
      expect(totalHidden.length).toEqual(0);
  })
});
