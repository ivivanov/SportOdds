import React, { Component } from 'react';
import Match from './MatchComponent';

class Event extends Component {

    constructor(props) {
        super(props);
        this.ws = props.ws;
        this.state = {
            id: props.data.id,
            name: props.data.name,
            matches: props.data.matches || []
        };
    }

    render() {
        return (
            <li className="list-group-item">
                <h5>{this.state.name}</h5>
                <ul className="list-group">
                    {this.state.matches.map((match, i) => (
                        <Match key={i} data={match} ws={this.ws}></Match>
                    ))}
                </ul>
            </li>
        );
    }
}

export default Event;
