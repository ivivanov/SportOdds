import React, { Component } from 'react';
import './app.css';
import * as signalR from '@aspnet/signalr';
import axios from 'axios';
import Event from './EventComponent';

const protocol = new signalR.JsonHubProtocol();
const transport = signalR.HttpTransportType.WebSockets;
const options = {
  transport,
  logMessageContent: true,
  logger: signalR.LogLevel.Trace
};
const ws = new signalR.HubConnectionBuilder()
  .withUrl('http://localhost:56348/sportUpdatesHub', options)
  .withHubProtocol(protocol)
  .build();

class App extends Component {

  constructor(props) {
    super(props);

    this.state = {
      events: [],
      test: "test"
    };

    this.onEventsAddReceived = this.onEventsAddReceived.bind(this);
    this.onEventsRemoveReceived = this.onEventsRemoveReceived.bind(this);
    this.onServerStatusReceived = this.onServerStatusReceived.bind(this);
  }

  componentDidMount() {
    var self = this;

    ws.on('EventsAdd', this.onEventsAddReceived);
    ws.on('EventsRemove', this.onEventsRemoveReceived);
    ws.on('ServerStatus', this.onServerStatusReceived);

    ws.start()
      .then(() => console.info("Connection succcess"))
      .catch(err => console.error('SignalR Connection Error: ', err));

    axios.get('http://localhost:56348/api/events')
      .then(function (response) {
        self.setState(function (prevState, props) {
          return {
            events: prevState.events.concat(response.data)
          };
        });
      })
      .catch(function (error) {
        console.log(error);
      });
  }

  onEventsAddReceived(data) {
    this.setState(function (prevState, props) {
      return {
        events: prevState.events.concat(data)
      };
    });
  }

  onEventsRemoveReceived(data) {
    //TODO
  }

  onServerStatusReceived(msg) {
  }

  componentWillUnmount() {
    this.connection.stop();
  }

  render() {

    return (
      <div className='app container'>
        <ul className="list-group">
          {this.state.events.map((event, i) => (
            <Event key={i} data={event} ws={ws}></Event>
          ))}
        </ul>
      </div>
    );
  }
}
export default App;
