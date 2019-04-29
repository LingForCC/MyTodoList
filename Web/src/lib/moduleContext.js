import React from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';

const ModuleContext = React.createContext(null);

const ModuleProvider = ({ module, children }) => (
  <ModuleContext.Provider value={module}>
    {children}
  </ModuleContext.Provider>
);

ModuleProvider.propTypes = {
  module: PropTypes.object.isRequired,
  children: PropTypes.node,
};
ModuleProvider.defaultProps = {
  children: null,
};

const connectModule = (moduleSelector) => {
  return Component => {
    const WithModule = connect(
      (_, parentProps) => moduleSelector(parentProps.module).getViewProps(parentProps),
    )(Component);
    return props => (
      <ModuleContext.Consumer>
        {
          module => (
            <WithModule
              module={module}
              {...props}
            />
          )
        }
      </ModuleContext.Consumer>
    );
  };
}

export {
  ModuleContext as default,
  ModuleProvider,
  connectModule,
};

