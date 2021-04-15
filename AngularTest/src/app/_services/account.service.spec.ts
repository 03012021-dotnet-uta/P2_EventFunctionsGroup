import { TestBed } from '@angular/core/testing';
import { Router } from '@angular/router';
import { AccountService } from './account.service';

describe('AccountService Test', () => {
  let service: AccountService;
  beforeEach(() => {
      TestBed.configureTestingModule({
          providers: [
              {
                provide: Router,
                useValue: { Router }
              },
              {
                  provide: AccountService,
                  useValue: { AccountService }
              }
          ]
        })
      service = TestBed.inject(AccountService);
  });

  fit('should be created', () => {
      expect(service).toBeTruthy();
  });
});
