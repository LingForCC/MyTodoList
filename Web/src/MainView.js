import React from 'react';
import { SceneView } from "@react-navigation/core";
import { Link } from "@react-navigation/web";

export default ({ descriptors, navigation }) => {
  const { key } = navigation.state.routes[navigation.state.index];
  const descriptor = descriptors[key];
  return (
    <div>
      <Link routeName={"Home"} navigation={navigation}>Home</Link>
      <SceneView
        navigation={descriptor.navigation}
        component={descriptor.getComponent()}
      />
    </div>
  );
};
