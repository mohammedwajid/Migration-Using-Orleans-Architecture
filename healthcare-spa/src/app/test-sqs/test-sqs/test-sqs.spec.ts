import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestSqs } from './test-sqs';

describe('TestSqs', () => {
  let component: TestSqs;
  let fixture: ComponentFixture<TestSqs>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TestSqs]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TestSqs);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
