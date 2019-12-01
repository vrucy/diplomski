import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrepravitiPonuduComponent } from './prepraviti-ponudu.component';

describe('PrepravitiPonuduComponent', () => {
  let component: PrepravitiPonuduComponent;
  let fixture: ComponentFixture<PrepravitiPonuduComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PrepravitiPonuduComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrepravitiPonuduComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
