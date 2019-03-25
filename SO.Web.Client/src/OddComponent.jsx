import React, { Component } from 'react';

class Odd extends Component {

    constructor(props) {
        super(props);
        this.ws = props.ws;
        this.state = {
            id: props.data.id,
            name: props.data.name,
            value: props.data.value
        };
        this.onOddsUpdatedReceived = this.onOddsUpdatedReceived.bind(this);
    }

    componentDidMount() {
        this.ws.on('oddsUpdated', this.onOddsUpdatedReceived);
    }

    onOddsUpdatedReceived(data) {
        this.setState(function (prevState, props) {
            for (let i = 0; i < data.length; i++) {
                const odd = data[i];
                if (odd.id == prevState.id) {
                    return {
                        value: odd.value
                    };
                }
            }
        });
    }

    render() {
        return (
            <li className='list-group-item'><em>{this.state.name}</em> - {this.state.value}</li>
        );
    }
}

export default Odd;
