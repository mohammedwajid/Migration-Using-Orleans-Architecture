import { Component } from '@angular/core';
import { SqsService } from '../../services/sqs.service';

@Component({
  selector: 'app-test-sqs',
  template: `<button (click)="send()" class="btn btn-success mb-3">Send Test via SQS</button>`
})
export class TestSqsComponent {
  constructor(private sqs: SqsService) {}

  send() {
    const request = {
      sessionRef: 'user-123',
      grainType: 'NotificationGrain',
      grainKey: 'user-123',
      methodName: 'AddNotification',
      payload: {
        id: '00000000-0000-0000-0000-000000000000',
        title: 'Test SQS',
        body: 'Hello from Angular via SQS',
        timestamp: new Date().toISOString()
      }
    };
    this.sqs.send(request).subscribe(() => console.log('sent'));
  }
}
