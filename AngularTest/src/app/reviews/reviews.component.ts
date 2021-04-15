import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Reviews } from '@app/_models';
import { EventService } from '@app/_services';

@Component({
  selector: 'reviews',
  templateUrl: './reviews.component.html',
  styleUrls: ['./reviews.component.less']
})
export class ReviewsComponent implements OnInit {
  @Input()
  id: string;
  public reviews: Reviews[];

  constructor(private eventService: EventService,
    private route: ActivatedRoute
    ) { }

  ngOnInit(): void {
    if (!this.id)
      this.id = this.route.snapshot.paramMap.get('id');
    this.eventService.getAllReviews(this.id).subscribe(x=> this.reviews = x);
    console.log(this.id);
  }

}
