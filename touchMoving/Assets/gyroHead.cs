// Copyright 2014 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using UnityEngine;

public class gyroHead : MonoBehaviour {

 
  public Transform target;

 
  public bool updateEarly = false;

 
  public Ray Gaze {
    get {
      UpdateHead();
      return new Ray(transform.position, transform.forward);
    }
  }

  private bool updated;

  void Update() {
    updated = false;  
    if (updateEarly) {
      UpdateHead();
    }
  }

  void LateUpdate() {
    UpdateHead();
  }

  private void UpdateHead() {
    if (updated) { 
      return;
    }
    updated = true;
    if (!Cardboard.SDK.UpdateState()) {
      return;
    }

    var rot = Cardboard.SDK.HeadRotation;
    if (target == null) {
      transform.localRotation = rot;
    } else {
      transform.rotation = rot * target.rotation;
    }
  }
}
