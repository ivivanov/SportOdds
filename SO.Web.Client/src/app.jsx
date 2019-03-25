import React, { Component } from 'react';
import './app.css';
import * as signalR from '@aspnet/signalr';
import axios from 'axios';
import Event from './EventComponent';
import Match from './MatchComponent';
import { debug } from 'util';

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
      matches: []
    };

    this.onMatchesAddedReceived = this.onMatchesAddedReceived.bind(this);
    this.onMatchesRemovedReceived = this.onMatchesRemovedReceived.bind(this);
  }

  componentDidMount() {
    var self = this;
    ws.on('matchesAdded', this.onMatchesAddedReceived);
    ws.on('matchesRemoved', this.onMatchesRemovedReceived);
    ws.start()
      .then(() => console.info("Connection succcess"))
      .catch(err => console.error('SignalR Connection Error: ', err));

    //Initial load
    axios.get('http://localhost:56348/api/matches')
      .then(function (response) {
        self.setState(function (prevState, props) {
          return {
            matches: prevState.matches.concat(response.data)
          };
        });
      })
      .catch(function (error) {
        console.log(error);
      });
  }

  onMatchesAddedReceived(data) {
    this.setState(function (prevState, props) {
      return {
        events: prevState.matches.concat(data)
      };
    });
  }

  onMatchesRemovedReceived(data) {
    //TODO
  }

  componentWillUnmount() {
    this.connection.stop();
  }

  render() {

    return (
      <div className='app'>
        <ul className="list-group">
          {this.state.matches.map((event, i) => (
            <Match key={i} data={event} ws={ws}></Match>
          ))}
        </ul>
      </div>
    );
  }
}
export default App;
